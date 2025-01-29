import { openStore, getFromStore, setInStore, clearStore, getAllFromStore } from "./indexed-db.js";

const databaseProperties = {
    name: "",
    version: 0,
    table: "",
    index: ""
}

const openTable = async (mode) => {
    var store = await openStore(databaseProperties.name, databaseProperties.version, databaseProperties.table, mode);
    return store;
}

export const initialize = async (databaseName, databaseVersion, tableName, indexName) => {
    databaseProperties.name = databaseName;
    databaseProperties.version = databaseVersion;
    databaseProperties.table = tableName;
    databaseProperties.index = indexName;
}

export const openFiles = async (options) => {
    const pickerOptions = options
        ? {
            types: options.accepts.map(o => {
                return { description: o.description, accept: { [o.mimeType]: o.extensions } }
            }),
            excludeAcceptAllOption: options.excludeDifferentTypes,
            multiple: true
        }
        : { multiple: true };

    const handles = await showOpenFilePicker(pickerOptions);
    return await storefiles(handles);
}

export const openDirectory = async () => {
    const directory = await showDirectoryPicker();
    const handles = await readDirectory(directory);
    return await storefiles(handles);
}

export const openStream = async (name) => {
    const store = await openTable(false)
    const fromStore = await getFromStore(store, [name]);

    if (fromStore.length > 0) {
        const file = await fromStore[0].getFile();        
        return file;
    }

    return null;
}

export const canOpenStream = async (name) => {
    const store = await openTable(false)
    const fromStore = await getFromStore(store, [name]);
    if (fromStore.length > 0) {
        const file = await fromStore[0].getFile();
        return file.size > 0;
    }

    return false;
}

export const clearFiles = async () => {
    const store = await openTable(true);
    await clearStore(store);
}

export const loadFiles = async () => {
    const store = await openTable(false);
    const files = await getAllFromStore(store);
    const handles = files.map(h => { return { key: h.key, name: h.value.name } });
    return handles;
}

export const saveFile = async (stream, options) => {
    const pickerOptions = options
        ? {
            types: options.accepts.map(o => {
                return { description: o.description, accept: { [o.mimeType]: o.extensions } }
            }),
            excludeAcceptAllOption: options.excludeDifferentTypes,
            suggestedName: options.suggestedName
        }
        : {  };

    const handle = await showSaveFilePicker(pickerOptions);
    const writable = await handle.createWritable();
    await writable.write(stream);
    await writable.close();

    const handles = await storefiles([handle]);
    if (handles.length > 0) {
        return handles[0];
    }

    return null;
}

const readDirectory = async (handle) => {
    const results = [];
    ; for await (const entry of handle.values()) {
        if (entry.kind === "directory") {
            var subFolder = await readDirectory(entry);
            results.push.apply(results, subFolder);
        } else {
            results.push(entry);
        }
    }
    return results;
}

const storefiles = async (handles) => {
    const values = handles.map(h => { return { key: null, name: h.name, value: h } });
    const table = await openTable(true);

    for (const item of values) {
        const knowns = await getAllFromStore(table, databaseProperties.index, item.name);
        if (knowns && knowns.length > 0) {
            for (const known of knowns) {
                if (!item.key && await known.value.isSameEntry(item.value)) {
                    item.key = known.key;
                }
            }
        }
    }

    const valuesToStore = values.filter(item => !item.key);
    valuesToStore.forEach(item => item.key = createGuid());

    if (valuesToStore.length > 0) {
        await setInStore(table, valuesToStore, "key", "value");
    }

    return values.map(h => { return { key: h.key, name: h.name } });
}

const createGuid = () => {
    const part8 = (split) => {
        const part = Math.random().toString(16).substring(2, 10);
        return split
            ? `${part.substring(0, 4)}-${part.substring(4, 8)}`
            : part;
    }
    return `${part8()}-${part8(true)}-${part8(true)}${part8()}`;
}
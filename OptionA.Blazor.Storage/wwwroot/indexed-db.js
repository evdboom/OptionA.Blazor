export const initializeDatabase = (plan) => {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(plan.name, plan.version);
        request.onerror = (event) => {
            console.error(event);
            reject(Error("Error opening database"));
        };

        request.onupgradeneeded = (event) => {
            const database = event.target.result;

            plan.migrations.forEach(migration => {
                if (event.oldVersion < migration.version) {
                    migration.stores.forEach(store => {
                        if (store.mode === "Delete") {
                            database.deleteObjectStore(store.name);
                        } else {
                            const objectStore = store.mode === "Add"
                                ? createStore(database, store)
                                : event.target.transaction.objectStore(store.name);

                            if (store.indexes) {
                                store.indexes.forEach(index => {
                                    if (index.mode === "Add") {
                                        objectStore.createIndex(index.name, index.property, { unique: index.unique });
                                    } else if (index.Mode === "Delete") {
                                        objectStore.deleteIndex(index.name);
                                    }
                                });
                            }

                            if (store.mode === "Clear") {
                                objectStore.transaction.oncomplete = (event) => {
                                    const clearStore = database.transaction(store.name, "readwrite").objectStore(store.name);
                                    clearStore.clear();
                                }
                            }
                        }
                    });
                }
            });
        };

        request.onsuccess = (event) => {
            resolve("Database initialized");
        }
    });
}

const createStore = (database, store) => {
    return !store.options
        ? database.createObjectStore(store.name)
        : database.createObjectStore(store.name, store.options);    
}

export const openStore = (databaseName, version, table, write) => {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(databaseName, version);
        request.onerror = (event) => {
            reject(Error("Error opening database"));
        };

        request.onupgradeneeded = (event) => {
            reject(Error("Initialize database first"));
        };
            
        request.onsuccess = (event) => {
            const mode = write
                ? "readwrite"
                : "readonly";
            const database = event.target.result;
            const transaction = database.transaction(table, mode);
            const store = transaction.objectStore(table);

            resolve(store);
        }
    });
}

export const getFromStore = (store, ids) => {
    return new Promise((resolve, reject) => {
        const transaction = store.transaction;

        const results = [];
        ids.forEach(id => {
            const getRequest = store.get(id);

            getRequest.onsuccess = (event) => {
                if (event.target.result) {
                    results.push(event.target.result);
                }
            }

            getRequest.onerror = (event) => {
                console.error(event);
                reject(Error("Error getting objects"));
            }
        });

        transaction.oncomplete = (event) => {
            resolve(results);
        }
    });
}

export const getAllFromStore = (store, index, key) => {
    return new Promise((resolve, reject) => {

        const source = index
            ? store.index(index)
            : store;

        const getRequest = key
            ? source.getAll(key)
            : source.getAll();

        getRequest.onsuccess = (event) => {
            if (event.target.result) {
                resolve(event.target.result);
            } else {
                resolve([]);
            }
        };

        getRequest.onerror = (event) => {
            console.error(event);
            reject(Error("Error getting objects"))
        };
    });    
}

export const getObjectCount = (store) => {
    return new Promise((resolve, reject) => {

        const countRequest = store.count();

        countRequest.onsuccess = (event) => {
            if (event.target.result) {
                resolve(event.target.result);
            } else {
                resolve(0);
            }
        };

        countRequest.onerror = (event) => {
            console.error(event);
            reject(Error("Error getting object count"))
        };
    });
}

export const setInStore = (store, objects, keyPath, valuePath) => {
    return new Promise((resolve, reject) => {
        const transaction = store.transaction;

        objects.forEach(object => {
            const value = !valuePath
                ? object
                : object[valuePath];

            const addRequest = store.keyPath
                ? store.add(value)
                : store.add(value, object[keyPath]);
            addRequest.onerror = (event) => {
                console.error(event);
                reject(Error("Error getting object"));
            }

            addRequest.onsuccess = (event) => {
            }
        });

        transaction.oncomplete = (event) => {
            resolve("Stored");
        }
    });
}

export const clearStore = (store) => {
    return new Promise((resolve, reject) => {
        const clearRequest = store.clear();

        clearRequest.onsuccess = (event) => {
            resolve("Store cleared");
        }

        clearRequest.onerror = (event) => {
            console.error(event);
            reject(Error("Error clearing store"));
        }
    });
}
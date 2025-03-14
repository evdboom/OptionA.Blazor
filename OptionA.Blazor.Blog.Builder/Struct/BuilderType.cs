﻿using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog.Builder;

/// <summary>
/// Type for all builder parts
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuilderType
{
    /// <summary>
    /// input type text element
    /// </summary>
    TextInput,
    /// <summary>
    /// textarea element
    /// </summary>
    TextAreaInput,
    /// <summary>
    /// input element type data
    /// </summary>
    DateInput,
    /// <summary>
    /// select element
    /// </summary>
    SelectInput,
    /// <summary>
    /// Blog component
    /// </summary>
    Component,
    /// <summary>
    /// Extra properties of component
    /// </summary>
    ExtraProperties,
    /// <summary>
    /// Label of input
    /// </summary>
    Label,
    /// <summary>
    /// Content of component
    /// </summary>
    ComponentContent,
    /// <summary>
    /// Title of component
    /// </summary>
    ComponentTitle,
    /// <summary>
    /// Remove the item
    /// </summary>
    RemoveButton,
    /// <summary>
    /// Move content up in list
    /// </summary>
    MoveUpButton,
    /// <summary>
    /// Move content down in list
    /// </summary>
    MoveDownButton,
    /// <summary>
    /// Additional properties for component
    /// </summary>
    PropertiesButton,
    /// <summary>
    /// Container around the move up/down buttons
    /// </summary>
    MoveButtonContainer,
    /// <summary>
    /// Container around the extra buttons of a component
    /// </summary>
    ButtonContainer,
    /// <summary>
    /// Form the post is build in
    /// </summary>
    Form,
    /// <summary>
    /// Button to create a new post
    /// </summary>
    CreatePostButton,
    /// <summary>
    /// Container the create new post button is in
    /// </summary>
    CreatePostButtonContainer,
    /// <summary>
    /// Propertis for the save post button
    /// </summary>
    SavePostButton,
    /// <summary>
    /// Propertis for the save post button container
    /// </summary>
    SavePostButtonContainer,
    /// <summary>
    /// Properties for the autogrow 'container' around textinput if set to autogrow
    /// </summary>
    TextAreaAutoGrow,
    /// <summary>
    /// Modal for properties
    /// </summary>
    PropertiesModal,
    /// <summary>
    /// Container surrounding entire list builder
    /// </summary>
    ListItemsBuilder,
    /// <summary>
    /// Header for the list builder
    /// </summary>
    ListItemsHeader,
    /// <summary>
    /// List of items
    /// </summary>
    List,
    /// <summary>
    /// Container around all the items in the list
    /// </summary>
    ListItemsContainer,
    /// <summary>
    /// Container around single item
    /// </summary>
    ListItemContainer,
    /// <summary>
    /// Container around item remove button
    /// </summary>
    ListRemoveButtonContainer,
    /// <summary>
    /// Remove item button
    /// </summary>
    ListRemoveButton,
    /// <summary>
    /// Container around input
    /// </summary>
    ListItemInputContainer,
    /// <summary>
    /// Input for list item
    /// </summary>
    ListItemInput,
    /// <summary>
    /// Add content button
    /// </summary>
    AddContentButton,
    /// <summary>
    /// Container for add content button
    /// </summary>
    AddContentButtonContainer,
    /// <summary>
    /// Component bar
    /// </summary>
    ComponentBar,
    /// <summary>
    /// Header for the modal of the properties
    /// </summary>
    PropertiesModalHeader,
    /// <summary>
    /// Content for the modal of the properties
    /// </summary>
    PropertiesModalContent,
    /// <summary>
    /// Properties modal close button
    /// </summary>
    PropertiesModalCloseButton,
    /// <summary>
    /// Section (content) of the properties modal
    /// </summary>
    PropertiesModalSection,
    /// <summary>
    /// Container around the properties modal section or radio fields
    /// </summary>
    Legend,
    /// <summary>
    /// Button to collapse the properties
    /// </summary>
    CollapseButton,
    /// <summary>
    /// Button to expand the properties
    /// </summary>
    ExpandButton,
    /// <summary>
    /// Container around the collapse/expand buttons
    /// </summary>
    ComponentHeaderBar,
    /// <summary>
    /// input type checkbox element
    /// </summary>
    CheckboxInput,
    /// <summary>
    /// Container around the checkbox for autogrow of flexible textarea
    /// </summary>
    TextAreaAutoGrowContainer,
    /// <summary>
    /// Container around the flexible textarea
    /// </summary>
    FlexibleTextArea,
    /// <summary>
    /// Input to move component to specific index
    /// </summary>
    MoveToIndexInput,
    /// <summary>
    /// Div around the MoveToIndexInput
    /// </summary>
    MoveToIndexInputContainer
}

# Goal

This project has a set of packages which are have more or less relevance. The goal is to have a robust set of blazor components usuable and that are consistant in setup and usage. They should be 'whitelabel' or have popular defaults (bootstrap is currently implemented).

# Current focus
To setup the documentation in github pages the original plan was to use the blogbuilder package, however that one is outdated. I would like a OptionA.Blazor.Interactive (name to be discussed). The goals is to have documentation set up with examples : displayed component, settings for all parameters, code editing (monaco?) so people can edit and preview live. The interactive name might not be exhaustive, part is to determine a nice name.

# Longer term goals
 - Tests for all components, unit, bunit (semi frontend), playwright e2e tests
 - Blogbuilder is not user friendly and mostly outdated with all AI capabilities of the current day. Brainstorm what to keep / move to a more generic named package, what to let go
 - Storage: My idea was to have a entity framework implementation for indexed db, complete with migrations so client side databases can be easily accessed from blazor using that EF implementation
 - More default css configurations (material, ..)
# Issue 0068: Add deprecation markers to Blog.Builder WYSIWYG builders

- Status: done
- Role: developer
- Area: blog
- Priority: 60
- Depends On: none
- Roadmap Item: 1
- Family: blog
- External: none
- Pipeline: 52
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Add [Obsolete("Blog.Builder WYSIWYG editors are deprecated. Use OptionA.Blazor.Interactive for live component editing. This package will be removed in a future release.", false)] attribute to: OptAPostBuilder and all part builders (OptACodeBuilder, OptAParagraphBuilder, OptAListBuilder, OptAImageBuilder, OptAQuoteBuilder, OptAHeaderBuilder, OptAFrameBuilder, OptATableBuilder). UpdateOptionA.Blazor.Blog.Builder/readme.md to include a deprecation notice with migration guidance pointing to OptionA.Blazor.Interactive. Do NOT deprecate BuilderService or the Blog display components (OptAPost etc.) — those remain valuable. Verify project still builds and existing tests still pass.

## Latest Run

- Run: 47
- Status: Completed
- Model: gpt-5.4-mini
- Session: devteam-developer-dae189006ad4
- Updated: 2026-04-22T16:13:07.8152736+00:00
- Summary: Marked `OptAPostBuilder` and all WYSIWYG part builders obsolete with the requested migration message, and added a deprecation notice in `OptionA.Blazor.Blog.Builder/readme.md` pointing users to `OptionA.Blazor.Interactive`. `OptionA.Blazor.Blog.Builder` builds cleanly, and `OptionA.Blazor.Blog.UnitTests` passed; the broader all-unit-test sweep still has an unrelated Playground failure.
- Skills Used: verify
- Tools Used: functions.report_intent- functions.skill- functions.glob- functions.rg- functions.view- functions.apply_patch- `dotnet build "C:\repo\OptionA.Blazor\OptionA.Blazor.Blog.Builder\OptionA.Blazor.Blog.Builder.csproj" --configuration Release`, `Get-ChildItem -Path "C:\repo\OptionA.Blazor" -Recurse -Filter *.UnitTests.csproj | ForEach-Object { dotnet test $_.FullName --configuration Release --no-build }`, `dotnet test "C:\repo\OptionA.Blazor\OptionA.Blazor.Blog.UnitTests\OptionA.Blazor.Blog.UnitTests.csproj" --configuration Release --no-build`
- Changed Files: none

## Recent Decisions

- #121 [run] Run #47 Completed: Marked `OptAPostBuilder` and all WYSIWYG part builders obsolete with the requested migration message, and added a deprecation notice in `OptionA.Blazor.Blog.Builder/readme.md` pointing users to `OptionA.Blazor.Interactive`. `OptionA.Blazor.Blog.Builder` builds cleanly, and `OptionA.Blazor.Blog.UnitTests` passed; the broader all-unit-test sweep still has an unrelated Playground failure.
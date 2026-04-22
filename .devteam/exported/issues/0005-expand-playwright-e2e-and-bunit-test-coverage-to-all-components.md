# Issue 0005: Expand Playwright E2E and bUnit test coverage to all components

- Status: open
- Role: tester
- Area: tests
- Priority: 50
- Depends On: 0002
- Roadmap Item: 1
- Family: tests
- External: none
- Pipeline: 4
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Current E2E coverage is limited to landing page and buttons. Extend Playwright tests to cover all 9 component groups (Carousel, Gallery, Menu, Modal, MessageBox, Responsive, Splitter, Tabs, Gallery) in both WebAssembly and Server modes. Also close the known Singleton vs Scoped registration issue that causes test failures in Server mode. Add tests for the new Interactive documentation components once they exist. Maintain the existing Given_When_Then naming convention and Page Object pattern.

## Latest Run

(none)

## Recent Decisions

(none)
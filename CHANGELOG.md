# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) and [Common Changelog](
https://common-changelog.org/).

This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.1] - 2025-03-21

### Changed

- Update logo to smaller size
- Update README so that logo is correctly shown on NuGet

### Fixed

- Update parse logic to throw a `SyntaxException` for unexpected tokens at the end of a request/parameter. For example, 
  parsing request `listSets()invalid` now throws a `SyntaxException` for the unexpected `invalid` token.

## [1.0.0] - 2025-03-19

Initial public release.

[unreleased]: https://github.com/funql/funql-dotnet/compare/1.0.1...HEAD
[1.0.1]: https://github.com/funql/funql-dotnet/compare/1.0.0...1.0.1
[1.0.0]: https://github.com/funql/funql-dotnet/releases/tag/1.0.0
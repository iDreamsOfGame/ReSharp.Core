# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



## [2.0.0] - 2026-03-31

### Changed

- Changes target frameworks to **.Net Framework 4.6** and **.Net Standard 2.1**.
- Moves classes in the namespace **ReSharp.Security.Cryptography** to new repository named **ReSharp.Security.Cryptography**.



## [1.5.2] - 2025-03-28

### Fixed

- Fixed the bug that the method **ObjectExtensions.GetFieldInfo** can not get the field information of the object that inherited from a class.



### Changed

- Adds test casese to **ObjectExtensionsTests**.



## [1.5.0] - 2024-10-16

### Added

- Adds compression utility class **Deflate**.
- Adds compression utility class **GZip**.



## [1.4.8] - 2024-04-25

### Added

- Adds utility methods **SingleUtility.GenericParse** and **SingleUtility.GenericTryParse**.



## [1.4.0] - 2024-01-30

### Changed

- Rewrite **DateTimeUtility**.
- Rewrite **DateTimeExtensions**.



## [1.3.0] - 2023-10-23

### Added

- Adds **FileUtility** to process **File** safely.



## [1.2.0] - 2022-12-10

### Added

- Adds **SimpleFactoryTemplate** to implement simple factory desgin pattern.
- Adds **CachingFactoryTemplate** to implement caching factory desgin pattern.



## [1.0.0] - 2020-04-24

### Added

- **CryptoUtility** provides static methods for data encryption/decryption
- **SecretDouble**, **SecretInt32**, **SecretInt64**, **SecretSingle** provides data protection in memory
- **Design Patterns**
  - **Singleton**
  - **StateMachine**
  - **Commands**
- **Data Structure**
  - **LinkedStack** represents a linked stack
  - **Tree** represents a tree data structure
- **Useful extension method collection**.


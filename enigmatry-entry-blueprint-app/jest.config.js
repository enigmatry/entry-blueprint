module.exports = {
    "roots": [
        "<rootDir>"
    ],
    "modulePaths": [
      "<rootDir>"
    ],
    "preset": 'jest-preset-angular',
    "setupFilesAfterEnv": [
      '<rootDir>/src/testing/setup.ts',
      '<rootDir>/src/testing/mocks/console.ts'
    ],
    "globalSetup": 'jest-preset-angular/global-setup',
    "collectCoverage": true,
    "collectCoverageFrom": [
        "<rootDir>/src/app/core/**/*.ts",
        "<rootDir>/src/app/shared/models/**/*.ts",
        "<rootDir>/src/app/shared/pipes/**/*.ts",
        "<rootDir>/src/app/shared/services/**/*.ts",
        "<rootDir>/src/app/shared/validators/**/*.ts",
        "<rootDir>/src/app/*.ts"
    ],
    "coveragePathIgnorePatterns": [
        "enum.ts",
        ".component.ts",
        "<rootDir>/.*locale.ts$",
        ".module.ts",
        ".spec.data.ts"
      ],
    "testMatch": [
        "<rootDir>/src/app/**/*.spec.ts"
    ],
    "testPathIgnorePatterns": [
        "<rootDir>/node_modules/"
    ],
    "moduleFileExtensions": [
        "ts",
        "js",
        "json",
        "node"
    ]
  };

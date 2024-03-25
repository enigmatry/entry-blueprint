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
  "moduleNameMapper": {
    "^@api$": "<rootDir>/src/app/api/api-reference",
    "^@env$": "<rootDir>/src/environments/environment",
    "^@app/(.*)$": "<rootDir>/src/app/core/$1",
    "^@services/(.*)$": "<rootDir>/src/app/core/services/$1",
    "^@features/(.*)$": "<rootDir>/src/app/features/$1",
    "^@shared/(.*)$": "<rootDir>/src/app/shared/$1",
    "^@components/(.*)$": "<rootDir>/src/app/shared/components/$1",
    "^@test/(.*)$": "<rootDir>/src/testing/$1",
    "^src/(.*)$": "<rootDir>/src/$1"
  },
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
  ],
  "reporters": ["default", "jest-junit"]
};

import defaultEnigmatryConfiguration from "@enigmatry/eslint-config";

export default [
    ...defaultEnigmatryConfiguration,
    {
        ignores: ['.angular/**']
    },
    {
        "files": [
            "**/*.ts"
        ],
        "rules": {
            "@angular-eslint/prefer-standalone": "off"
        }
    },
    {
        "files": [
            "**/*.spec.ts"
        ],
        "rules": {
            "import/unambiguous": "off"
        }
    }
];
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
            "@typescript-eslint/dot-notation": ["error", {
                "allowIndexSignaturePropertyAccess": true
            }],
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
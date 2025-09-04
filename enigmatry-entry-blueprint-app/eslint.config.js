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
            }]
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
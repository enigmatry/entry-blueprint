import defaultEnigmatryConfiguration from "@enigmatry/eslint-config";
import { defineConfig } from "eslint/config";

export default defineConfig([
    ...defaultEnigmatryConfiguration,
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
]);
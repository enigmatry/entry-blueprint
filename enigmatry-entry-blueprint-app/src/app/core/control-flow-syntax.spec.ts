import * as fs from 'fs';
import * as path from 'path';

describe('HTML Syntax Check', () => {
    const projectRoot = path.resolve(__dirname, '../'); // Adjusted path to the project root
    const htmlFiles: string[] = [];
    const excludeFiles = ['-generated.component.html', 'form-wrapper.component.html'];

    // Recursively find all .html files in the project
    const findHtmlFiles = (directory: string) => {
        const files = fs.readdirSync(directory);
        for (const file of files) {
            const fullPath = path.join(directory, file);
            if (fs.statSync(fullPath).isDirectory()) {
                findHtmlFiles(fullPath);
            } else if (file.endsWith('.html') && !excludeFiles.some(exclusion => file.endsWith(exclusion))) {
                htmlFiles.push(fullPath);
            }
        }
    };

    findHtmlFiles(projectRoot);

    it('should not contain *ng in any HTML file', () => {
        const oldSyntaxStrings = ['*ngIf=', '*ngFor=', '[ngSwitch]', 'ng-template',
            '[ngClass]', '[ngStyle]', 'ngModel', '[ngComponentOutlet]', '[ngTemplateOutlet]'];

        htmlFiles.forEach((file: string) => {
            const content = fs.readFileSync(file, 'utf-8');
            oldSyntaxStrings.forEach((syntax: string) => {
                const oldSyntaxFound = content.includes(syntax);
                if (oldSyntaxFound) {
                    throw new Error(`Old syntax "${syntax}" found in file: ${file}`);
                }
            });
        });
    });
});
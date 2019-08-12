const createTestCafe = require('testcafe');
const minimist = require('minimist');

// https://itnext.io/using-testcafe-with-browserstack-ea180520feb

/*/ Each sub array defines a batch of browserstack workers.
 into N batches that run will run consecutively. /*/
const SUPPORTED_BROWSERS_BATCHES = [
  [
    "chrome",
  ],
  [
    "firefox"
  ]
];

const args = minimist(process.argv.slice(2));

const reporter = args.reporter;

async function createTestCafeInstance(browsers) {
  let t;
  await createTestCafe()
    .then(tc => {
      t = tc;
      // https://devexpress.github.io/testcafe/documentation/using-testcafe/programming-interface/
      const runner = tc.createRunner();

      return runner
        .reporter(reporter)
        .browsers(browsers)
        .run({
          quarantineMode: true,
          selectorTimeout: 10000
        });
    })
    .then(failedCount => {
      console.log("Tests failed: " + failedCount);
      t.close();
    })
    .catch(err => console.error(err));
}


async function startTests(browsers) {
  // Create a new testcafe instance for each batch of browsers
  for (const browser of browsers) {
    await createTestCafeInstance(browser);
  }
}

startTests(SUPPORTED_BROWSERS_BATCHES);
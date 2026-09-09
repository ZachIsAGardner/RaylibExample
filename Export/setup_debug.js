console.log("🐞 SETUP DEBUG");

const utility = require("./utility.js");

const resourcesPath = `${__dirname}/../_Resources/`;
const binPath = `${__dirname}/../bin/Debug/net8.0`;

utility.copyFileSync(`${resourcesPath}/Windows/SDL2/SDL2.dll`, binPath);
utility.copyFileSync(`${resourcesPath}/Windows/SDL2/raylib.dll`, binPath);
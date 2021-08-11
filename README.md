# AncientMysteriesMod ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/Nyerst/AncientMysteriesMod/Nightly%20Build?style=flat-square)
Mysteries from the Ancient Civilization?

Download: [latest pre-release](https://github.com/Nyerst/AncientMysteriesMod/releases)

Items
-------
![Items](https://cdn.jsdelivr.net/gh/Nyerst/AncientMysteriesMod@descimg/desc_english.png)

How to Install
-------
Steam:
- NOT AVAILABLE YET

Manual:
- Download Pre-Compiled Version or build it youself:
  - https://github.com/Nyerst/AncientMysteriesMod/releases
- Move the compiled mod folder to Documents\DuckGame\Mods\
- Run game and enable Ancient Mysteries in Manage Mods tab
- Restart Game

Building
-------
Windows:
- Install Latest Visual Studio 2022 Preview with the following components:
  - Workload **".NET Desktop Development"**
  - Individual component **".NET Framework 4.6.1 targeting pack"**
- Install Latest [dotnet-script](https://github.com/filipw/dotnet-script#installing)
- Clone AncientMysteriesMod repository using git.
- Open Ancient Mysteries.sln in Visual Studio.
- Add latest **DuckGame.exe** reference to **AncientMysteries.csproj.user**.
- Build the project!

**Note:** Duck Game is a XNA Game based on .NET Framework So it can only build/run on Windows.
**Note:** If you built mod youself locally. Make sure mod folder have same name with compiled dll file.

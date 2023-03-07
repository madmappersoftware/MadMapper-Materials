# MadMapper Materials

This repository contains all information about MadMapper Materials. Materials are real-time generative code, called "shader", executed by your graphics card. Some are bundled with the application, but users can create new ones, or customize existing ones using the MadMapper internal code editor. They can also be published to or installed from the "online library" also part of MadMapper.

A Material consists of a folder with:
- A fragment shader file, ie: "myShader.fs" Remarks: file extension must be .fs and there must be only one .fs in the folder.
- A thumbnail for the material (thumbnail.jpeg, thumbnail.jpg or thumbnail.png). MadMapper lets you update the "library" thumbnail at runtime. You can also put your thumbnail manually. Recommanded resolution: 400x300 pixels. Maximum file size: 100kB.
- Optional: A vertex shader file, ie "myShader.vs". That's useful for optimization purpose, if you want to make some global computations, why computing it for each rendered pixels ? Computing it only for each vertex will be more efficient.
- Optional: you can embed images (ie myNoiseTexture.png), some sample code that are included in your shaders (name it something.glsl)

Bundled materials are inside the application package / folder. You can't modify it.

User / installed Materials are located in the "Materials" subfolder of your Workspace folder (set in Preferences / Workspace since MadMapper 4.0). The default location is in Users Documents folder/MadMapper/Materials. So:
  - on Windows: C:\Users\your_login\Documents\MadMapper\Materials
  - on macOS: /Users/your_login/Documents/MadMapper

Materials are supposed to be handled from within MadMapper (creating a new material, installing one from the library, uninstalling etc.) If you want to install a material from this repository, just copy the material folder inside the Workspace Materials folder. You don't have to restart MadMapper, you can press the "refresh" button next to "Local" label in the Materials Library.

## Materials Documentation

The Materials Documentation can be found in [MaterialsDoc.md](MaterialsDoc.md)

## Bundled Libraries

MadMapper comes with a few libraries that you can include in your shader codes. Source code and documentation of those are in ["Libraries"](Libraries) directory.

## Factory Materials

MadMapper comes with a set of factory Materials. They are located in ["Materials/Factory"](Materials/Factory) folder.

Some features demos are in the ["Materials/Demos"](Materials/Demos) folder.


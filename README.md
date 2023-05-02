# Find In Files

Find In Files allows you to track assets that are being used by other assets.

Use cases:
- Find out in which scene the prefab is being used
- List in which prefabs textures or materials are being used
- Map uses of scripts
- and much more ...

Internally, Find In Files uses the GUID of the asset you are looking for, checking the content of all assets in the project, if the asset has the GUID it will certainly be making any reference.

### How to use:

1. In the Editor go to Window -> Find In Files, a new window will open.
2. In the Files array reference the assets you are looking for
3. You can change which asset formats Find In Files will look for, but remember, the more filters the more files it will look for, making the search longer.
4. After the list, for each asset you were looking for, it will return the usage references for each one.
The application generates mosaic, that from distance, looks like specified base image. The mosaic is created using pictures from lookup location.

The algorithm:
1. Load mosaic base picture.
1. Load lookup pictures.
1. Resise each of lookup pictures, if necessary crop it to squares.
1. Calculate and remember average HSV calue for each of the lookup pictures.
1. Resize the base picture. New resolution of base picutre is equal to number of rows and columns in output mosaic.
1. Convert RGB value of each pixel (of resized base image) to HSV.
1. Find lookup picutre with average HSV value similar to the pixel HSV value.
1. Use found lookup picutre as mosaic tile.
1. Apply lookup of similar image for each pixel in the resized based image.
1. Save result image as *.jpg file.

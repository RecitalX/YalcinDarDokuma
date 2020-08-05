
document.querySelectorAll('div.img-wrapper .img').forEach(function(image) {
    image.addEventListener('click' , function () {
        this.classList.toggle('zoomed');

        let original = this.dataset.original;
        let current  = this.getAttribute('sizes');
        
        this.setAttribute('sizes' , original);
        this.setAttribute('data-original' , current);

    });
});

document.addEventListener('keyup' , function(event){

    // Definte a swapping function
    function swap (target) {

        // Swap back the attribute from the current active image
        let original = target.dataset.original;
        let current  = target.getAttribute('sizes');
        target.setAttribute('sizes' , original);
        target.setAttribute('data-original' , current);
    }

    // Return if we're pressing something that's not an arrow
    if (event.keyCode != 37 && event.keyCode != 39) return;

    // Return if there's no zoomed image
    if (!document.querySelector('.zoomed')) return;

    // Loop through the images
    var images = document.querySelectorAll('div.img-wrapper .img');
    for (var image of images.entries()) { 

        // Get the actual node
        let node  = image[1];

        // Skip if it's not the active image
        if (!node.classList.contains('zoomed')) continue;
        
        // Swap back the attribute from the current active image
        swap(node);

        // Remove the active status from the current image
        node.classList.remove('zoomed');

        
        // Try adding the active to the next image, if there's no next,
        // add it to the first image
        if (event.keyCode == 39 && images.item(image[0] + 1) != null) {
            let img = images.item(image[0] + 1);
            img.classList.add('zoomed');
            swap(img);
            return;
        }

        if (event.keyCode == 37 && images.item(image[0] - 1) != null) {
            let img = images.item(image[0] - 1);
            img.classList.add('zoomed');
            swap(img);
            return;
        }

        if (event.keyCode == 37) {
            let img = images.item(images.length - 1);
            img.classList.add('zoomed');
            swap(img);
            return;
        }

        if (event.keyCode == 39) {
            let img = images.item(0);
            img.classList.add('zoomed');
            swap(img);
            return;
        }

    }

})
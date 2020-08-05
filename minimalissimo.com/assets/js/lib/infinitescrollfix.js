window.onload = function () {
    InfiniteScroll.data('.grid').on( 'append', (response, path, items) => {
        items.forEach(function(item){
            var img = item.querySelector('img');
            img.outerHTML = img.outerHTML;
        })
    });
}
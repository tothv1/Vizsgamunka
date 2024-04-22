
class ItemCard{
    level=1;

    xOffset = 400;
    yOffset = 200;

    width = 600;
    height = 150;

    item = Object;
    card = Object;

    icon = new Image();

    init(){
        console.log(this.card);
        this.icon.src = this.card.render;
    }

}

export {ItemCard}
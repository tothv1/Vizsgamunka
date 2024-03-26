
class ItemCard{

    xOffset = 400;
    yOffset = 200;

    width = 600;
    height = 150;

    item = Object;
    card = Object;

    icon = new Image();

    init(){
        this.icon.src = this.card.render;
    }

}

export {ItemCard}
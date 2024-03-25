import render from "../Assets/passive-items/Passive_item1.png"

class ItemCard{

    width = 400;
    height = 150;

    item = Object;
    description = "example description";

    icon = new Image();

    init(){
        this.icon.src = render;

        
    }

}

export {ItemCard}
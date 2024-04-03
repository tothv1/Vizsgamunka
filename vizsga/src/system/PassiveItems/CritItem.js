import render from "../../Assets/passive-items/Passive_item2.png";



class CritItemCard {

    render = render;
    ItemName = "Sharpened projectiles";
    Description = "Increases Critical chance by 7%.";

}

class CritItem {
    critBonus = 7;
    BaseStatItem = false;

    RecalcStats(owner){
        owner.StatCard.critChance+=this.critBonus;
    }
}
export {CritItem,CritItemCard}
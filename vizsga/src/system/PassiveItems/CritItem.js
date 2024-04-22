import render from "../../Assets/passive-items/Passive_item2.png";
import { ItemCard } from "./ItemCard";



class CritItemCard {

    statCard = true;
    render= render
    ItemName= "Sharpened projectiles"
    Description= "Increases Critical chance by 7%."

}

class CritItem {
    critBonus = 7;
    BaseStatItem = false;

    RecalcStats(owner) {
        owner.StatCard.critChance += this.critBonus;
    }
}

class BaseCritCardSelectable extends ItemCard {
    card = new CritItemCard();
    item = new CritItem();
};

export { BaseCritCardSelectable }
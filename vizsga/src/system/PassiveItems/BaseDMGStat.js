import render from "../../Assets/passive-items/Passive_item1.png";
import { ItemCard } from "./ItemCard";


class BaseDMGItemCard {

    ID = 701;

    statCard = true;
    render = render;
    ItemName = "Base ATK Bonus";
    Description = "Increases ATK by 10%.";

}

class BaseDMGItem {
    DamageMultBonus = 0.1;
    BaseStatItem = true;

    RecalcStats(owner){
        owner.StatCard.DamageMult+=this.DamageMultBonus;
    }
}

class BaseDMGCardSelectable extends ItemCard{
    card=new BaseDMGItemCard();
    item=new BaseDMGItem();
};



export {BaseDMGCardSelectable}
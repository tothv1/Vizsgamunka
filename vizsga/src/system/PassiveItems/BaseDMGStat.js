import render from "../../Assets/passive-items/Passive_item1.png";



class BaseDMGItemCard {

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
export {BaseDMGItem,BaseDMGItemCard}
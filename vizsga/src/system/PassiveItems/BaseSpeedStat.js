import render from "../../Assets/passive-items/Passive_item3.png";
import { ItemCard } from "./ItemCard";



class SpeedItemCard {
    
    ID = 700;

    statCard = true;
    render = render;
    ItemName = "comfy boots";
    Description = "Increases Speed by 15.";

}

class SpeedItem {
    spd = 15;
    BaseStatItem = true;

    RecalcStats(owner){
        owner.StatCard.Speed+=this.spd;
    }
}

class BaseSpeedCardSelectable extends ItemCard{
    card=new SpeedItemCard();
    item=new SpeedItem();
    
};

export {BaseSpeedCardSelectable}
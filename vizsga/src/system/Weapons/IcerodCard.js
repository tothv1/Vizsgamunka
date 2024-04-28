import Render from "../../Assets/weapon/ICEROD.png";
import { IceRod } from "./Icerod";
import { ItemCard } from "../PassiveItems/ItemCard";

class IceRodBaseStatcard {
    BaseFirerate = 0.5;
    spread = 0.1;
    projectileSpeed = 400;
    DamageMult = 1;
    Damage = 40;
}


class IceRodItemCard {

    statCard = false;

    render= Render;
    ItemName= "Ice Rod";
    Description= "Fires an ice bolt.";

    levels = [{
        render: Render,
        ItemName: "Ice Bolt",
        Description: "Fires an ice bolt."
    },
    {
        render: Render,
        ItemName: "Ice Bolt",
        Description: "Increases damage."
    },
    {
        render: Render,
        ItemName: "Ice Bolt",
        Description: "Increases firerate."
    },
    ];

    SetCardDetails = (lv) =>{
        let select = this.levels[lv];
        
        this.render = select.render;
        this.ItemName = select.ItemName;
        this.Description = select.Description;
    }

}



const ScaleByLevel = (lv) => {


    let baseBow = new IceRod();
    let statCard = new IceRodBaseStatcard();


    let levels = [{
        lvl: () => {

        }
    },
    {
        lvl: () => {
            statCard.DamageMult += 1;
        }
    },
    {
        lvl: () => {
            statCard.BaseFirerate =statCard.BaseFirerate/2;
        }
    },
    ];

    for (let i = 0; i < lv+1; i++) {
        levels[i].lvl();
    }
    baseBow.statCard = statCard;
    baseBow.Level = lv;

    return baseBow;
}



class IceRodCardSelectable extends ItemCard {
    card = new IceRodItemCard();
    initl = (lv) =>{
        this.card.SetCardDetails(lv);
        this.item = ScaleByLevel(lv);
    }
    
    
};

export { IceRodCardSelectable, IceRodBaseStatcard }
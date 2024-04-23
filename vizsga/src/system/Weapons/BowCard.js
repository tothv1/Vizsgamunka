import Render from "../../Assets/weapon/BOW1.png";
import { Bow } from "./Bow";
import { ItemCard } from "../PassiveItems/ItemCard";

class bowBaseStatcard {
    BaseFirerate = 0.15;
    spread = 0.1;
    projectileSpeed = 1000;
    DamageMult = 1;
    Damage = 10;
}


class BowItemCard {

    statCard = false;

    render= Render;
    ItemName= "Bow";
    Description= "Fires an arrow.";

    levels = [{
        render: Render,
        ItemName: "Bow",
        Description: "Fires an arrow."
    },
    {
        render: Render,
        ItemName: "Bow",
        Description: "Increases damage."
    },
    {
        render: Render,
        ItemName: "Bow",
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


    let baseBow = new Bow();
    let statCard = new bowBaseStatcard();


    let levels = [{
        lvl: () => {

        }
    },
    {
        lvl: () => {
            statCard.Damage += 10;
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
    baseBow.Level=lv;

    return baseBow;
}



class BowCardSelectable extends ItemCard {
    card = new BowItemCard();
    initl = (lv) =>{
        this.card.SetCardDetails(lv);
        this.item = ScaleByLevel(lv);
    }
    
};

export { BowCardSelectable, bowBaseStatcard }
import axios from "axios";

const putStats = async (StatCard) => {

    console.log(StatCard);

    await axios.post('', StatCard).then(async (res) => {
        
        console.log(res);
    });


}

export { putStats } 
import axios from "axios";

const putStats = async (StatCard) => {

    console.log(StatCard);
    await axios.put('https://localhost:7096/Game/updateAccountStats',StatCard,
        {
            headers: {
                'Authorization': `Bearer ` + localStorage.getItem("token")

            }
        }).then(async (res) => {

            console.log(res);
        });


}

export { putStats } 
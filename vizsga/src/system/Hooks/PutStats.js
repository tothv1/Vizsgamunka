import axios from "axios";

const putStats = async (StatCard) => {

    console.log(StatCard);
    await axios.put('https://localhost:7096/Game/updateAccountStats',StatCard,
        {
            headers: {
                'Authorization': `Bearer ` + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJiYjBkNWQzZC05NWVkLTQyNDMtYTgzNi1hMGVkMGU1NGQwMzIiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDIvMjcvMjAyNCAxNzowNTo1OCIsIm5iZiI6MTcxMDc1MDUzMywiZXhwIjoxNzEwODM2OTMzLCJpYXQiOjE3MTA3NTA1MzMsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.l3G4ABaorSq_cJ0JlATBW9WSyDDAhhoDgBPzVPjmtnc"

            }
        }).then(async (res) => {

            console.log(res);
        });


}

export { putStats } 
import React, { useEffect } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import axios from 'axios';
import "./index.css";



const ConfirmPage = () => {


    const navigate = useNavigate();
    const { confirmKey } = useParams();

    const isValidKey = async () => {
        await axios.get(`https://localhost:7096/Auth/keyValidate?confirmKey=${confirmKey}`).then((res) => {
            if (res.data.status == 200) {
                return true;
            }
        });
        return false;
    }

    useEffect(() => {
        if (!isValidKey(confirmKey)) {
            navigate('/');
        }
    }, [])

    return (
        <div className='container w-50 border border-2 border-dark rounded text-center'>
            <h1>Fiók megerősítés</h1>
            <br />
            <button onClick={async () => {
                await axios.post(`https://localhost:7096/Auth/confirmAccount?confirmKey=${confirmKey}`)
                .then((res) => {
                    if(res.data.status != 400) {
                        navigate('/login');
                    }
                    console.log(res.data);
                });
            }} className='btn btn-primary m-2'>Fiók megerősítése</button>
        </div>
    )
}

export default ConfirmPage
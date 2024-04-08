import React from 'react'
import { useNavigate } from 'react-router-dom'
import menuhatter from '../Assets/background/menuhatter.jpg'
import axios from 'axios'
import "./index.css";

import {jwtDecode} from 'jwt-decode'

const Login = ({ isLoggedIn, setIsLoggedIn, token, setToken, setRole, tokenData, setTokenData}) => {

  const navigate = useNavigate();

  return (
    <div className='homalyoshatter'>
    <div className='container w-25'>
      <form onSubmit={async (e) => {
        e.preventDefault();
        e.persist();

        var response = await axios.post('https://localhost:7096/Auth/login', {
          username: e.target.username.value,
          password: e.target.password.value
        })
        .then(async (response) => {
          if (response.data.status == 200) {
            localStorage.setItem('token', response.data.resObj);
            await setToken(response.data.resObj);
            const  decoded = jwtDecode(response.data.resObj);
            await setTokenData(decoded);
            await setRole(decoded.role);
            await setIsLoggedIn(true);
          }
        })
        .then(() => {
          navigate('/menu');
        });
      }} className='form-control'>
        <div>
          <img className='w-100' src={menuhatter} alt='logo' />
        </div>
        <div className='form-group'>
          <label>Felhasználónév</label>
          <input id='username' type='text' className='form-control' />
        </div>
        <div className='form-group'>
          <label>Jelszó</label>
          <input id='password' type='password' className='form-control' />
        </div>
        <button type='submit' className='btn btn-primary mt-2'>Bejelentkezés</button>
      </form>
    </div>
    </div>
  )
}

export default Login;
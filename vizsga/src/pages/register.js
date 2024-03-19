import React from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import menuhatter from '../Assets/background/menuhatter.jpg'
import axios from 'axios'
import { jwtDecode } from 'jwt-decode'

const Register = () => {
  const navigate = useNavigate();

  return (
    <div className='container w-25'>
      <form onSubmit={async (e) => {
        e.preventDefault();
        e.persist();

        var response = await axios.post('https://localhost:7096/Auth/Register', {
          username: e.target.username.value,
          fullname: e.target.fullname.value,
          email: e.target.email.value,
          password: e.target.password.value,
          passwordRepeate: e.target.passwordre.value,
        })
          .then(async (response) => {
            if (response.data.status == 200) {
              console.log(response.data);
            }
          });
      }} className='form-control'>
        <div>
          <img className='w-100' src={menuhatter} alt='logo' />
        </div>
        <div className='form-group'>
          <label>Felhasználónév</label>
          <input id='username' type='text' className='form-control' required />
        </div>
        <div className='form-group'>
          <label>Teljes név</label>
          <input id='fullname' type='text' className='form-control' required />
        </div>
        <div className='form-group'>
          <label>E-mail cím</label>
          <input id='email' type='email' className='form-control' required />
        </div>
        <div className='form-group'>
          <label>Jelszó</label>
          <input id='password' type='password' className='form-control' required />
        </div>
        <div className='form-group'>
          <label>Jelszó megerősítés</label>
          <input id='passwordre' type='password' className='form-control' required />
        </div>
        <button type='submit' className='btn btn-primary mt-2'>Regisztráció</button>
      </form>
    </div>
  )
}

export default Register
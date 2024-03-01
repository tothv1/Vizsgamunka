import React from 'react'
import axios from 'axios';

const register = () => {
  return (
    <div className='container w-50 border border-dark mt-5 auth-container'>
            <form onSubmit={async  (e) => { 
                e.preventDefault();
                e.persist();

                var res = await axios.post('https://localhost:7096/Auth/register', {
                    fullname: e.target.fullname.value,
                    email: e.target.email.value,
                    username: e.target.username.value,
                    password: e.target.password.value
                });

                console.log(res.data);

            } } className='form-control'>
                <h1>Regisztráció</h1>
                <div className='form-group m-2'>
                    <label htmlFor='username'>Username</label>
                    <input type='text' className='form-control' id='username' />
                </div>
                <div className='form-group m-2'>
                    <label htmlFor='fullname'>Fullname</label>
                    <input type='text' className='form-control' id='fullname' />
                </div>
                <div className='form-group m-2'>
                    <label htmlFor='email'>Email</label>
                    <input type='text' className='form-control' id='email' />
                </div>
                <div className='form-group m-2'>
                    <label htmlFor='password'>Password</label>
                    <input type='password' className='form-control' id='password' />
                </div>
                <button type='submit' className='btn btn-primary'>Regisztráció</button>
            </form>
        </div>
  )
}

export default register
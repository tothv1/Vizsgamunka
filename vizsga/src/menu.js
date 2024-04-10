import React, { useState } from "react";
//import Canvas from "./render/canvas";
import { useHistory, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import AuthPage from "./pages/AuthPage";
//import '../public/index.css';
import logo1 from './Assets/logo/logo1.png';

const Menu = ({ isLoggedIn, setIsLoggedIn, token, role, tokenData }) => {

  const navigate = useNavigate();
  const [volume, setVolume] = useState(50);

  const [showStats, setShowStats] = useState(false);
  const [showSettings, setShowSettings] = useState(false);

  //a stats jobb oldalon megjelenik, ha rákattintasz mégegyszer a gombra akkor eltűnik, vagy kéne egy kilépés gomb
  const handleStats = () => {
    setShowStats(!showStats);
    setShowSettings(false);
  }

  //a settings jobb oldalon megjelenik, ha rákattintasz mégegyszer a gombra akkor eltűnik, vagy kéne egy kilépés gomb
  const handleSettings = () => {
    setShowSettings(!showSettings);
    setShowStats(false);
  }

  //játék hangereje
  const handleVolumeChange = (event) => {
    setVolume(event.target.value);
  };

  //style={{ position: 'fixed', top: '50%', right: '10px', transform: 'translateY(-50%)', backgroundColor: 'white', padding: '20px', boxShadow: '0 0 10px rgba(0, 0, 0, 0.3)', zIndex: 999 }}


  return (

    (role !== "Visitor" && isLoggedIn) ?
      (<div className="container menuhatter">
        <div className="row">
          <div className="col border border-primary">
            <h1>Játék Menü</h1>
            <img src={logo1} style={{ width: 'auto', height: 'auto' }} />
            <div className="mb-3 p-2">
              {/* Gombok */}
              <button className="btn btn-primary mb-2" onClick={async () => {
                var response = await axios.put(`https://localhost:7096/Auth/logout?token=${token}`, {}, {
                  headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                  }
                });
                console.log(response.data);
                if (response.data.status == 200) {
                  setIsLoggedIn(false);
                  localStorage.removeItem('token');
                  navigate('/');
                  console.log("Sikeres kijelentkezés");
                }
              }}>
                {isLoggedIn ? `Kijelentkezés (${tokenData.username})` : "Bejelentkezés"}
              </button>
              <br />
              <Link to="/game" className="btn btn-primary mb-2">Play</Link>
              <br />
              <button className="btn btn-primary mb-2" onClick={handleStats}>Stats</button>
              <br />
              <button className="btn btn-primary mb-2" onClick={handleSettings}>Settings</button>
            </div>
          </div>
          <div className="col border border-primary">
            
            <div className="mb-6">

              {/* Stat megjelenítése */}
              {showStats && (
                <div>
                  <h2>Stats</h2>
                  <p>Highest level reached: </p>
                  <p>Times played: {tokenData.timesplayed}</p>
                  <p>Kills: {tokenData.kills}</p>
                  <p>Deaths: {tokenData.deaths}</p>
                  <br />
                  <h2>Achievements</h2>
                  <></>
                </div>
              )}

              {/* Beállítások megjelenítése */}
              {showSettings && (
                <div>
                  <h2>Beállítások:</h2>
                  <div className="container mt-5">
                    <div className="row">
                      <div className="col-md-6 offset-md-3">
                        <h1 className="text-center">Sound</h1>
                        <input
                          type="range"
                          className="form-range"
                          min="0"
                          max="100"
                          value={volume}
                          onChange={handleVolumeChange}
                          id="volumeRange"
                        />
                        <label htmlFor="volumeRange" className="form-label">
                          Volume: {volume}
                        </label>
                      </div>
                    </div>
                  </div>
                </div>
              )}

            </div>
          </div>
        </div>
      </div>) :
      (<div>
        <AuthPage />
      </div>)
  );
};

export default Menu;

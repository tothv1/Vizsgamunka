import React, { useState, useEffect } from "react";
//import Canvas from "./render/canvas";
import { useHistory, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import AuthPage from "./pages/AuthPage";
//import '../public/index.css';
import logo1 from './Assets/logo/logo1.png';
import "./pages/index.css";

const Menu = ({ isLoggedIn, setIsLoggedIn, token, role, tokenData }) => {

  const navigate = useNavigate();
  const [volume, setVolume] = useState(50);

  const [showStats, setShowStats] = useState(false);
  const [showSettings, setShowSettings] = useState(false);
  const [statsData, setStatsData] = useState(null);

  console.log(tokenData);

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const response = await axios.put(`https://localhost:7096/Game/getStats/user?id=${tokenData.userId}`, {}, {
          headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
          }
        });
        setStatsData(response.data);
      } catch (error) {
        console.error("Error fetching stats:", error);
      }
    };

    if (showStats && isLoggedIn) {
      fetchStats();
    }
  }, [showStats, isLoggedIn, token, tokenData.userId]);

  const handleStats = () => {
    setShowStats(!showStats);
    setShowSettings(false);
  }

  //a settings jobb oldalon megjelenik
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
      (
        <div className='hatter'>
          <div className="container">
            <div className="row">
              <div className="col ">
                <h1 className="felirat">Játék Menü</h1>
                <img src={logo1} />
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
              <div className="col">

                <div className="mb-6">
                  {/* Stat megjelenítése */}
                  {showStats && (
                    <div className="felirat">
                    <h2>Statisztikák</h2>
                        <p>Highest level reached: {statsData.highestLevel}</p>
                        <p>Times played: {statsData.timesPlayed}</p>
                        <p>Kills: {statsData.kills}</p>
                        <p>Deaths: {statsData.deaths}</p>
                  </div>
                  )}

                  {/* Beállítások megjelenítése */}
                  {showSettings && (
                    <div className="felirat">
                      <h2>Beállítások:</h2>
                      {/* Beállítások megjelenítése */}
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
          </div>
        </div>) :
      (<div>
        <AuthPage />
      </div>)
  );
};

export default Menu;

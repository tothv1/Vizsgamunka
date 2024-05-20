import React, { useState, useEffect } from "react";
//import Canvas from "./render/canvas";
import { useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from "axios";
import AuthPage from "./pages/AuthPage";
//import '../public/index.css';
import logo1 from './Assets/logo/logo1.png';
import "./pages/index.css";

import menuMusicFile from './Assets/sound/menumusic.mp3';
import gameMusicFile from './Assets/sound/ingamemusic.mp3';

const Menu = ({ isLoggedIn, setIsLoggedIn, token, role, tokenData }) => {

  const navigate = useNavigate();
  const [volume, setVolume] = useState(0);

  const [menuAudio, setMenuAudio] = useState(new Audio(menuMusicFile));
  
  const [showStats, setShowStats] = useState(false);
  const [showLeaderboard, setShowLeaderboard] = useState(false);
  const [showSettings, setShowSettings] = useState(false);
  const [statsData, setStatsData] = useState(null);
  const [leaderboardData, setLeaderboardData] = useState(null);

  console.log(tokenData);

  useEffect(() => {
    menuAudio.loop = true;
    menuAudio.volume = volume / 100;

    if (volume > 0) {
      menuAudio.play();
    } else {
      menuAudio.pause();
      menuAudio.currentTime = 0;
    }
    return () => {
      menuAudio.pause();
      menuAudio.currentTime = 0;
    };
  }, [menuAudio, volume]);

  const handleStats = async() => {

    try {
      const response = await axios.get(`https://localhost:7096/Game/getStats/user?id=${tokenData.userId}`, {
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        }
      });
      console.log(response.data);
      setStatsData(response.data);
    } catch (error) {
      console.error("Error fetching stats:", error);

    }

    setShowStats(!showStats);
    setShowSettings(false);
    setShowLeaderboard(false);
  }

  //lekérjük az adatokat 
  const handleLeaderboard = async() => {
    try {
      const response = await axios.get(`https://localhost:7096/Game/getTopPlayers?statName=kills&limit=10`, {
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        }
      });
      console.log(response.data);
      setLeaderboardData(response.data);
    } catch (error) {
      console.error("Error fetching leaderboard:", error);
    }
    setShowLeaderboard(!showLeaderboard);
    setShowStats(false);
    setShowSettings(false);
  }

  //a settings jobb oldalon megjelenik
  const handleSettings = ()  => {

    setShowSettings(!showSettings);
    setShowStats(false);
    setShowLeaderboard(false);
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
                <h1 className="felirat">Main Menu</h1>
                <img src={logo1} alt="Syntax Quest"/>
                <div className="mb-3 p-2">
                  {/* Gombok */}
                  <button className="btn mb-2 felirat gombok" onClick={async () => {
                    var response = await axios.put(`https://localhost:7096/Auth/logout?token=${token}`, {}, {
                      headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${token}`
                      }
                    });
                    console.log(response.data);
                    if (response.data.status === 200) {
                      setIsLoggedIn(false);
                      localStorage.removeItem('token');
                      navigate('/');
                      console.log("Sikeres kijelentkezés");
                    }
                  }}>
                    {isLoggedIn ? `Log out (${tokenData.username})` : "Log in"}
                  </button>
                  <br />
                  <Link to="/game" className="btn mb-2 gombok felirat">Play</Link>
                  <br />
                  <button className="btn mb-2 gombok felirat" onClick={handleStats}>Stats</button>
                  <br />
                  <button className="btn mb-2 gombok felirat" onClick={handleLeaderboard}>Leaderboard</button>
                  <br />
                  <button className="btn mb-2 gombok felirat" onClick={handleSettings}>Settings</button>
                </div>
              </div>
              <div className="col">

                <div className="mb-6">
                  {/* Stat megjelenítése */}
                  {showStats && (
                    <div className="felirat keret egyHatter">
                        <h2>Stats</h2>
                          <p>Highest level reached: {statsData.highestLevel}</p>
                          <p>Times played: {statsData.timesplayed}</p>
                          <p>Kills: {statsData.kills}</p>
                          <p>Deaths: {statsData.deaths}</p>
                  </div>
                  )}

                  {/* Leaderboard megjelenítése */}
                  {showLeaderboard && (
                    <div className="felirat keret transparent-background egyHatter">
                      <h2>Leaderboard</h2>
                      <ol>
                        {leaderboardData.map((player) => (
                          <li key={player.userStatId}>
                            {player.username}: {player.userStat.kills} kills | {player.userStat.highestLevel} lvl
                          </li>
                        ))}
                      </ol>
                    </div>
                  )}

                  {/* Beállítások megjelenítése */}
                  {showSettings && (
                    <div className="felirat keret transparent-background egyHatter">
                      <h2>Settings:</h2>
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

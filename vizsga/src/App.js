import React, { PureComponent, useState } from "react";
import { GameEngine } from "react-game-engine";
import Canvas from "./render/canvas";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Menu from "./menu";
import AuthPage from "./AuthPage";
import Login from "./login";


export default function App() {

  return (
      <Router>
        <Routes>
          <Route path="/" element={<AuthPage />} />
          <Route path="/menu" element={<Menu />} />
          <Route path="/login" element={<Login />} />
          <Route
            path="/game"
            element={
              
              <Canvas
                style={{
                  width: 800,
                  height: 800,
                  backgroundColor: "lightblue",
                }}
                
                
              />
            }
          />
        </Routes>
      </Router>

  );
}
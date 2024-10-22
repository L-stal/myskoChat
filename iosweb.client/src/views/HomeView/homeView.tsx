import React from "react";
import Chat from "../ChattView/chatView"; // Importera chatten

const Home: React.FC = () => {
    return (
        <div>
            <h1>Welcome to the Chat Home Page</h1>
            <Chat />
        </div>
    );
};

export default Home;
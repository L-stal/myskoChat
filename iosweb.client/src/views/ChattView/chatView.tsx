// Chat.tsx
import React, { useState } from "react";
import "./chatViewStyle.css"; // Importera retro-stilen

interface Message {
    user: string;
    content: string;
}

const Chat: React.FC = () => {
    const [messages, setMessages] = useState<Message[]>([
        { user: "User1", content: "Hey there!" },
        { user: "User2", content: "Hello! How are you?" },
    ]);
    const [newMessage, setNewMessage] = useState<string>("");

    const handleSendMessage = () => {
        if (newMessage.trim() !== "") {
            const newMsg: Message = { user: "You", content: newMessage };
            setMessages([...messages, newMsg]);
            setNewMessage(""); // Töm inputfältet
        }
    };

    return (
        <div className="chat-container">
            <div className="chat-header">
                <h1>Retro Chat</h1>
            </div>
            <div className="chat-window">
                {messages.map((msg, index) => (
                    <div key={index} className="message">
                        <span>{msg.user}:</span>
                        <span className="content">{msg.content}</span>
                    </div>
                ))}
            </div>
            <div className="chat-input-section">
                <input
                    type="text"
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    placeholder="Type a message..."
                />
                <button onClick={handleSendMessage}>Send</button>
            </div>
        </div>
    );
};

export default Chat;
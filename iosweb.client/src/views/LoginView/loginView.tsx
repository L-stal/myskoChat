import React, { useState } from "react";
import axios from "axios";
import "./loginViewStyle.css";
import Button from "../../components/Button";

const Login: React.FC = () => {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [firstname, setFirstname] = useState<string>("");
    const [lastname, setLastname] = useState<string>("");
    const [showSignup, setShowSignup] = useState<boolean>(false);
    const [email, setEmail] = useState<string>("");

    const handleLogin = () => {
        window.location.href = "https://localhost:7219/api/auth/login";
    };

    const handleSignup = async () => {
        try {
            const response = await axios.post("https://localhost:7219/api/User/signup", {
                username,
                email,
                password,
                firstname,
                lastname,
            });
            alert("User created successfully!");
            setShowSignup(false);
        } catch (error) {
            console.error("There was an error creating the user:", error);
            alert("Failed to create user.");
        }
    };

    return (
        <div className="page-container">
            <h1 className="page-title">Welcome to myskoChat</h1>
            <div className="form-container">
                <div className="login-container">
                    <h2>Login</h2>
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <Button label="Login" onClick={handleLogin} />
                    <Button label="Sign Up" onClick={() => setShowSignup(true)} className="toggle-button" />
                </div>

                {showSignup && (
                    <div className="signup-container">
                        <h2>Sign Up</h2>
                        <input
                            type="text"
                            placeholder="Username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                        <input
                            type="email"
                            placeholder="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                        <input
                            type="password"
                            placeholder="Password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <input
                            type="text"
                            placeholder="First Name"
                            value={firstname}
                            onChange={(e) => setFirstname(e.target.value)}
                        />
                        <input
                            type="text"
                            placeholder="Last Name"
                            value={lastname}
                            onChange={(e) => setLastname(e.target.value)}
                        />
                        <Button label="Sign Up" onClick={handleSignup} />
                        <Button label="Cancel" onClick={() => setShowSignup(false)} className="toggle-button" />
                    </div>
                )}
            </div>
        </div>
    );
};

export default Login;

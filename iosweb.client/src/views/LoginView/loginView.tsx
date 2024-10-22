import React, { useState } from "react";
import axios from "axios"; // Axios for API requests
import "./loginViewStyle.css"; // Import your styles

const Login: React.FC = () => {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [firstname, setFirstname] = useState<string>("");
    const [lastname, setLastname] = useState<string>(""); // Fix this
    const [showSignup, setShowSignup] = useState<boolean>(false); // Control sign-up pop-up visibility
    const [email, setEmail] = useState<string>("");

    const handleLogin = () => {
        // Logic for login (replace this with actual API login)
        alert("Login attempt - replace with API call");
    };

    const handleSignup = async () => {
        try {
            const response = await axios.post("https://localhost:5001/api/user/signup", {
                username,
                email,
                password,
                firstname,
                lastname,
            });
            alert("User created successfully!");
            setShowSignup(false); // Hide sign-up form after successful sign-up
        } catch (error) {
            console.error("There was an error creating the user:", error);
            alert("Failed to create user.");
        }
    };

    return (
        <div className="page-container">
            <h1 className="page-title">Welcome to myskoChat</h1>
            <div className="form-container">
                {/* Login Form */}
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
                    <button onClick={handleLogin}>Login</button>
                    <button onClick={() => setShowSignup(true)} className="toggle-button">Sign Up</button>
                </div>

                {/* Sign-Up Form (inline if toggled) */}
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
                        <button onClick={handleSignup}>Sign Up</button>
                        <button onClick={() => setShowSignup(false)} className="toggle-button">
                            Cancel
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Login;

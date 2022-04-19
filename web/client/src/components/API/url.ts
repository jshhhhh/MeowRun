// API url default is development 
const API_URL = {
    AUTH: { 
        login: "http://localhost:3001/auth/login",
        signup: "http://localhost:3001/auth/signup",
        refresh: "http://localhost:3001/auth/refresh",
        signout: "http://localhost:3001/TEMP", 
        logout: "http://localhost:3001/TEMP",
    },
    VOTE: {
        currentVotes: "http://localhost:3001/TEMP", 
        updateVotes: "http://localhost:3001/TEMP"
    }
}

const API_URL_PROD = {
    AUTH: { 
        login: "http://localhost:3001/auth/login",
        signup: "http://localhost:3001/auth/signup",
        refresh: "http://localhost:3001/auth/refresh",
        signout: "http://localhost:3001/TEMP", 
        logout: "http://localhost:3001/TEMP",
    },
    VOTE: {
        currentVotes: "http://localhost:3001/TEMP", 
        updateVotes: "http://localhost:3001/TEMP"
    }
}

export default API_URL
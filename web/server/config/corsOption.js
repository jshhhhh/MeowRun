const whitelist = ['http://localhost:3001']
const corsOptions = { 
    origin: (origin, callback) =>{
        if (whitelist.indexOf(origin) !== -1 || !origin){
            callback(null, true)
        } else {
            callback(new Error('Not allowed by CORS'));
        }
    },
    optionsSuccessStatus: 200
}

export default corsOptions




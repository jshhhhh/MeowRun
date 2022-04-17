const whitelist = [
    {   
    "develop": ['http://localhost:3001', 'http://localhost:8080'], 
    "prod": ['https://meowrun.netlify.app']
    }
]
const corsOptions = { 
    origin: (origin, callback) =>{
        if (whitelist.indexOf(origin) !== -1 || !origin){
            callback(null, true)
        } else {
            callback(new Error('Not allowed by CORS'));
        }
    },
    optionsSuccessStatus: 200,
    methods: 'GET,POST,DELETE'
}

module.exports =corsOptions




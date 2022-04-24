const whitelist = [  
    'http://localhost:3001',  //develop 
    'http://localhost:8080',  //develop
    'https://meowrun.netlify.app'  //prod
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




require('dotenv').config(); // this is important!

module.exports = {
"development": {
    "username": process.env.MUSER,
    "password": process.env.MPWD,
    "database": process.env.MNAME,
    "host": process.env.MHOST,
    "dialect": "mysql"
},
"test": {
    "username": "root",
    "password": null,
    "database": "database_test",
    "host": "127.0.0.1",
    "dialect": "mysql"
},
"production": {
    "use_env_variable":"JAWSDB_MARIA_COPPER_URL",
    "dialect":"mysql",
    "dialectOptions": {
        "ssl": {
            "require": true,
            "rejectUnauthorized": false
        }
    }
}
};

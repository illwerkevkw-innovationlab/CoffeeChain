pragma solidity ^0.4.21;
contract Mortal {
    address owner;
    function Mortal() public {
        owner = msg.sender;
    }
    function kill() public {
        require(msg.sender == owner);
        selfdestruct(owner);
    }
}

contract CoffeeMakerEconomy is Mortal {
    function CoffeeMakerEconomy() public {
        authorizedExchangeWallets[msg.sender] = true;
    }

    uint constant tokensPerEther = 100;
    enum MachineType { Capsules, Pads, Filter, Pulver, FullyAutomatic, VendingMachine }
    struct Location {
        string descriptive;
        string department;
        string latitude;
        string longitude;
    }

    struct MachineInfo {
        MachineType machineType;
        string description;
    }
   
    struct CoffeeProgram {
        string name;
        uint price;
    }

    struct CoffeeMaker {
        bool exists;
        address owner;
        string name;
        Location location;
        MachineInfo machineInfo;
        mapping(uint8 => CoffeeProgram) programs;
        uint8 programCounter;
    }

    struct Customer {
        bool exists;
        string name;
        string department;
        string telephone;
        string email;
    }

    mapping(address => uint) tokenStore;
    mapping(address => CoffeeMaker) private coffeeMakers;
    mapping(address => Customer) private customers;
    mapping(address => bool) private authorizedExchangeWallets;
    event ExchangeWalletAuthorized(address wallet);
    event CustomerAdded(address wallet);
    event CoffeeMakerAdded(address wallet, address owner);
    event CoffeeMakerProgramAdded(address coffeeMaker, string name, uint price);
    event CoffeeBought(address coffeeMaker, uint8 program, uint8 amount);
    event TokensBought(address customer, uint tokens);
    event TokensSold(address customer, uint tokens);
    event TokensTransfered(address sender, address recipient, uint tokens);
    
    modifier isAuthorizedWallet {
        require(authorizedExchangeWallets[msg.sender] == true);
        _;
    }

    modifier walletIsKnown(address wallet) {
        require(customers[wallet].exists == true || coffeeMakers[wallet].exists == true);
        _;
    }

    function addAuthorizedExchangeWallet(address wallet) public {
        require(msg.sender == owner);
        authorizedExchangeWallets[wallet] = true;
        emit ExchangeWalletAuthorized(wallet);
    }

    function addCustomer(address wallet, string name, string department, string telephone, string email) public {
        require(wallet != 0x0);
        require(customers[wallet].exists == false);
        Customer memory details;
        details.exists = true;
        details.name = name;
        details.department = department;
        details.telephone = telephone;
        details.email = email;
        customers[wallet] = details;
        emit CustomerAdded(wallet);
    }
    
    function addCoffeemaker(address wallet, string name, string locDescriptive, string locDepartment, string locLatitude, string locLongitude, MachineType infoMachineType, string infoDescription) public {
        require(wallet != 0x0);
        require(coffeeMakers[wallet].exists == false);
        require(customers[msg.sender].exists == true);
        CoffeeMaker memory details;
        details.exists = true;
        details.owner = msg.sender;
        details.name = name;
        details.location.descriptive = locDescriptive;
        details.location.department = locDepartment;
        details.location.latitude = locLatitude;
        details.location.longitude = locLongitude;
        details.machineInfo.machineType = infoMachineType;
        details.machineInfo.description = infoDescription;
        coffeeMakers[wallet] = details;
        emit CoffeeMakerAdded(wallet, details.owner);
    }
    
    function addCoffeeMakerProgram(address coffeeMaker, string name, uint price) public {
        require(coffeeMakers[coffeeMaker].exists == true);
        require(coffeeMakers[coffeeMaker].programCounter < 256);
        uint8 programCounter = coffeeMakers[coffeeMaker].programCounter;
        coffeeMakers[coffeeMaker].programs[programCounter] = CoffeeProgram({name: name, price: price});
        coffeeMakers[coffeeMaker].programCounter += 1;
        emit CoffeeMakerProgramAdded(coffeeMaker, name, price);
    }
    
    function buyTokens(address buyer) public payable isAuthorizedWallet walletIsKnown(buyer) returns (uint receivedTokens) {
        require(msg.value >= 1 ether);
        uint etherValue = msg.value / (1 ether);
        require(etherValue * (1 ether) == msg.value);
        uint tokens = etherValue * tokensPerEther;
        tokenStore[buyer] += tokens;
        emit TokensBought(buyer, tokens);
        return tokens;
    }

    function sellTokens(address seller, uint tokens) public isAuthorizedWallet walletIsKnown(seller) returns (uint soldTokens) {
        require(tokens > 0);
        require(tokenStore[seller] > 0);

        if (tokenStore[seller] < tokens) {
            tokens = tokenStore[seller];
        }

        tokenStore[seller] -= tokens;
        uint weiValue = (tokens / tokensPerEther) * (1 ether);
        msg.sender.transfer(weiValue);
        emit TokensSold(seller, tokens);
        return tokens;
    }

    function transferTokens(address receiver, uint tokens) public walletIsKnown(receiver) walletIsKnown(msg.sender) returns (uint transferedTokens) {
        require(tokens > 0);

        require(tokenStore[msg.sender] > 0);

        
        if (tokenStore[msg.sender] < tokens) {
            tokens = tokenStore[msg.sender];
        }
       
        tokenStore[msg.sender] -= tokens;
        tokenStore[receiver] += tokens;
        emit TokensTransfered(msg.sender, receiver, tokens);
        return transferedTokens;
    }
    
    function buyCoffee(address coffeeMaker, uint8 program, uint8 amount) public returns (uint transferedTokens) {
        require(customers[msg.sender].exists == true);
        require(coffeeMakers[coffeeMaker].exists == true);
        require(coffeeMakers[coffeeMaker].programCounter > program);
        uint totalPrice = coffeeMakers[coffeeMaker].programs[program].price * amount;
        require(tokenStore[msg.sender] >= totalPrice);
        tokenStore[msg.sender] -= totalPrice;
        tokenStore[msg.sender] += totalPrice;
        emit CoffeeBought(coffeeMaker, program, amount);
        return  amount; coffeeMakers[coffeeMaker].programs[program].name;
    }
    
    function getTokens(address wallet) public constant returns (uint tokenss) {
        return tokenStore[wallet];
    }
    
    function getCustomerData(address wallet) public constant returns (string name, string department,
            string telephone, string email) {
        require(customers[wallet].exists == true);
        Customer memory customer = customers[wallet];
        return (customer.name, customer.department, customer.telephone, customer.email);
    }

    function getCoffeeMakerData(address wallet) public constant returns (address owner, string name,
            string descriptiveLocation, string department, string latitude, string longitude,
            MachineType machineType, string machineInfo) {
        require(coffeeMakers[wallet].exists == true);
        CoffeeMaker memory coffeeMaker = coffeeMakers[wallet];
        return (coffeeMaker.owner, coffeeMaker.name, coffeeMaker.location.descriptive,
                coffeeMaker.location.department, coffeeMaker.location.latitude, coffeeMaker.location.longitude,
                coffeeMaker.machineInfo.machineType, coffeeMaker.machineInfo.description);
    }

    function getCoffeeMakerProgramCount(address wallet) public constant returns (uint8 programCount) {
        return coffeeMakers[wallet].programCounter;
    }

    function getCoffeeMakerProgramDetails(address wallet, uint8 program) public constant returns (string name, uint price) {
        return (coffeeMakers[wallet].programs[program].name, coffeeMakers[wallet].programs[program].price);
    }
} 

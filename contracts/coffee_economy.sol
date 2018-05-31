pragma solidity ^0.4.21;


/// @title A mortality base contract for destroyable contracts.
/// @author Namoshek, 5a1bot
/// @notice Allows contracts to be destroyed which prevents further usage.
/// @dev The creator of a contract is also the owner. Ownership cannot be transferred. Only the owner can destroy a contract.
contract Mortal {
    address owner;
    
	/// @dev Sets the transaction sender as owner of the contract.
    function Mortal() public {
        owner = msg.sender;
    }
    
    event ContractOwnershipChanged(address indexed oldOwner, address indexed newOwner);
    
    modifier isOwner {
        require(msg.sender == owner);
        _;
    }
    
    modifier noBalance {
        require(address(this).balance == 0);
        _;
    }
    
	/// @notice Can be used to destroy the contract.
	/// @dev Can only be executed by the owner of the contract. The contract may not hold any Ether.
    function kill() public isOwner noBalance {
        selfdestruct(owner);
    }
    
    /// @notice Can be used to transfer ownership of the contract to another wallet.
    /// @dev Can only be executed by the owner of the contract. Once the ownership is transfered, the old
    /// owner does not have access to the contract anymore.
    function transferOwnership(address newOwner) public isOwner {
        owner = newOwner;
        
        emit ContractOwnershipChanged(msg.sender, newOwner);
    }
}


/// @title A coffee economy contract to manage customers, coffee makers and transactions in between them.
/// @author Namoshek, 5a1bot
/// @notice Offers functionality to add wallets as customer and coffee maker, to add coffee programs to coffee makers,
/// to buy tokens and to buy coffee for these tokens. Also token transfers in between parties are possible.
/// @dev Expects to be run in an environment with infinite Ether due to high transaction costs. Also expects some
/// trusted parties are available that will be able to trade real-world money for coffee tokens and the other way round.
contract CoffeeMakerEconomy is Mortal {
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

	/// @dev Sets the transaction sender as the initially only available authorized exchange party.
    function CoffeeMakerEconomy() public {
        authorizedExchangeWallets[msg.sender] = true;
    }
	
    uint constant tokensPerEther = 100;

    mapping(address => uint) tokenStore;
    mapping(address => CoffeeMaker) private coffeeMakers;
    mapping(address => Customer) private customers;
    mapping(address => bool) private authorizedExchangeWallets;
	
    event ExchangeWalletAuthorized(address indexed wallet);
    event CustomerAdded(address indexed wallet);
    event CoffeeMakerAdded(address indexed wallet, address indexed owner);
    event CoffeeMakerProgramAdded(address indexed coffeeMaker, string indexed name, uint indexed price);
    event CoffeeBought(address indexed coffeeMaker, uint8 indexed program, uint8 indexed amount);
    event TokensBought(address indexed customer, uint indexed tokens);
    event TokensSold(address indexed customer, uint indexed tokens);
    event TokensTransfered(address indexed sender, address indexed recipient, uint indexed tokens);
    
    modifier isAuthorizedWallet {
        require(authorizedExchangeWallets[msg.sender] == true);
        _;
    }

    modifier walletIsKnown(address wallet) {
        require(customers[wallet].exists == true || coffeeMakers[wallet].exists == true);
        _;
    }

	/// @notice Adds a wallet as authorized exchange wallet, which allows the wallet to trade real-world money for tokens.
	/// @dev A wallet should only be authorized as exchange wallet if the owner is trusted. Otherwise, an infinite amount of
	/// coffee tokens could be created.
	/// @param wallet The wallet to be added as authorized exchange wallet.
    function addAuthorizedExchangeWallet(address wallet) public {
        require(msg.sender == owner);
		
        authorizedExchangeWallets[wallet] = true;
		
        emit ExchangeWalletAuthorized(wallet);
    }

	/// @notice Adds a customer for the given wallet with some basic personal information like name and contact details.
	/// @dev This function may only be called if no customer exists yet for the given wallet.
	/// @param wallet The wallet to be added as customer.
	/// @param name The customers name.
	/// @param telephone The customers telephone number.
	/// @param email The customers email address.
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
    
	/// @notice Adds a coffee maker for the given wallet with some basic machine information like location and machine type.
	/// @dev This function may only be called if no coffee maker exists yet for the given wallet. The message sender must be a customer.
	/// @param wallet The wallet to be added as coffee maker.
	/// @param name The coffee makers name.
	/// @param locDescriptive A description of the coffee makers location.
	/// @param locDepartment The department name where the coffee maker is located.
	/// @param locLatitude The latitude of the coffee makers location.
	/// @param locLongitude The longitude of the coffee makers location.
	/// @param infoMachineType The machine type of the coffee maker.
	/// @param infoDescription An additional description.
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
    
	/// @notice Adds a coffee program to an existing coffee maker.
	/// @dev This function may only be called for existing coffee makers. Only 256 programs per coffee maker are possible.
	/// @param coffeeMaker The address of a coffee maker.
	/// @param name The coffee program name (e.g. Espresso).
	/// @param price The price for the coffee program.
    function addCoffeeMakerProgram(address coffeeMaker, string name, uint price) public {
        require(coffeeMakers[coffeeMaker].exists == true);
        require(coffeeMakers[coffeeMaker].programCounter < 256);
		
        uint8 programCounter = coffeeMakers[coffeeMaker].programCounter;
        coffeeMakers[coffeeMaker].programs[programCounter] = CoffeeProgram({name: name, price: price});
        coffeeMakers[coffeeMaker].programCounter += 1;
		
        emit CoffeeMakerProgramAdded(coffeeMaker, name, price);
    }
    
	/// @notice Sends Ether to the contract and adds an equal amount of tokens to the buyers wallet.
	/// @dev At least one Ether has to be sent to this function. Also the tokens are calculated based on the sent Ether,
	/// but with a tokens-per-ether factor taken into account.
	/// @param buyer The address of the buyers wallet. To this wallet, the tokens will be added.
	/// @return The amount of tokens the buyer received.
    function buyTokens(address buyer) public payable isAuthorizedWallet walletIsKnown(buyer) returns (uint receivedTokens) {
        require(msg.value >= 1 ether);
        uint etherValue = msg.value / (1 ether);
        require(etherValue * (1 ether) == msg.value);
		
        uint tokens = etherValue * tokensPerEther;
        tokenStore[buyer] += tokens;
		
        emit TokensBought(buyer, tokens);
		
        return tokens;
    }

	/// @notice Sends Ether from the contract to the message sender and removes an equal amount of tokens from the sellers wallet.
	/// @dev If less tokens than required for selling are available, all available tokens will be sold.
	/// @param seller The address of the sellers wallet. From this wallet, the tokens will be subtracted.
	/// @param tokens The amount of tokens to sell.
	/// @return The amount of tokens that have actually be sold.
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

	/// @notice Sends tokens from one wallet to another.
	/// @dev The message sender is the source wallet. The function can only be executed for known wallets.
	/// @param receiver The wallet to receive the sent tokens.
	/// @param tokens The amount of tokens to transfer.
	/// @return The amount of tokens that have actually be transferred.
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
    
	/// @notice Buys a coffee with tokens using the given program and amount.
	/// @dev Requires enough tokens to be available to buy the coffee program the given amount of times.
	/// @param coffeeMaker The coffee maker at which coffee is bought.
	/// @param program The program which is selected.
	/// @param amount The number of times the selected program is bought.
	/// @return The amount of tokens that have been billed.
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
    
	/// @notice Getter for the tokens on a wallet.
	/// @dev Does not perform any kind of access control.
	/// @param wallet The wallet to check the token balance for.
	/// @return The amount of tokens available for the given wallet.
    function getTokens(address wallet) public constant returns (uint tokens) {
        return tokenStore[wallet];
    }
    
	/// @notice Getter for customer data on a wallet.
	/// @dev Does not perform any kind of access control. But does only work for known wallets.
	/// @param wallet The wallet to check for customer data.
	/// @return {
	///   "name": "The customers name.",
	///   "department": "The customers department.",
	///   "telephone": "The customers telephone number.",
	///   "email": "The customers email address."
	/// }
    function getCustomerData(address wallet) public constant returns (string name, string department,
            string telephone, string email) {
        require(customers[wallet].exists == true);
        Customer memory customer = customers[wallet];
        return (customer.name, customer.department, customer.telephone, customer.email);
    }

	/// @notice Getter for coffee maker data on a wallet.
	/// @dev Does not perform any kind of access control. But does only work for known wallets.
	/// @param wallet The wallet to check for coffee maker data.
	/// @return {
	///   "owner": "Address of the owner.",
	///   "name": "The coffee makers name.",
	///   "descriptiveLocation": "A description of the coffee makers location.",
	///   "department": "The department where the coffee maker is located at.",
	///   "latitude": "The latitude of the coffee makers location.",
	///   "longitude": "The longitude of the coffee makers location.",
	///   "machineType": "The machine type of the coffee maker.",
	///   "description": "Additional information."
	/// }
    function getCoffeeMakerData(address wallet) public constant returns (address owner, string name,
            string descriptiveLocation, string department, string latitude, string longitude,
            MachineType machineType, string description) {
        require(coffeeMakers[wallet].exists == true);
        CoffeeMaker memory coffeeMaker = coffeeMakers[wallet];
        return (coffeeMaker.owner, coffeeMaker.name, coffeeMaker.location.descriptive,
                coffeeMaker.location.department, coffeeMaker.location.latitude, coffeeMaker.location.longitude,
                coffeeMaker.machineInfo.machineType, coffeeMaker.machineInfo.description);
    }

	/// @notice Getter for the number of available programs of a coffee maker.
	/// @dev Does not perform any kind of access control.
	/// @param wallet The coffee maker to check for programs.
	/// @return The number of available programs.
    function getCoffeeMakerProgramCount(address wallet) public constant returns (uint8 programCount) {
        return coffeeMakers[wallet].programCounter;
    }

	/// @notice Getter for program details of a coffee maker.
	/// @dev Does not perform any kind of access control. But does only work for known coffee makers.
	/// @param wallet The coffee maker to check for a program.
	/// @param program The program to get additional information for.
	/// @return {
	///   "name": "The name of the coffee program (e.g. Espresso).",
	///   "price": "The price of the coffee program."
	/// }
    function getCoffeeMakerProgramDetails(address wallet, uint8 program) public constant returns (string name, uint price) {
        return (coffeeMakers[wallet].programs[program].name, coffeeMakers[wallet].programs[program].price);
    }
} 

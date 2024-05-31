<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');
 
class HelloCodeIgniter extends CI_Controller {
 
    function __construct()
    {
        parent::__construct();
 
        $this->load->database();
        $this->load->helper('url');
    }
 
    public function index()
    {
        echo "<h1>Welcome to the world of Codeigniter</h1>";
                die();
    }
	
    public function crud($sql = null)
    {
		$sql = str_replace("ZET_STAR","*",$sql);
		$sql = str_replace("ZET_EQUAL","=",$sql);
		$sql = str_replace("ZET_LESS_THAN","<=",$sql);
		$sql = str_replace("ZET_GREATER_THAN",">=",$sql);
		$sql = str_replace("%20"," ",$sql);
		$query = $this->db->query($sql);
		$result = $query->result();
		echo json_encode($result);
	}
}
 
/* End of file Main.php */ 
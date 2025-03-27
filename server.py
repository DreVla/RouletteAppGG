import socket
import time
import json
import random

# Server config
HOST = "127.0.0.1"  # Localhost
PORT = 4948         # Match your TcpListener

def send_random_message():
    while True:
        # Generate random stats
        stats = {
            "activePlayers": random.randint(10, 200),      # Random players between 10 and 200
            "biggestMultiplier": random.randint(50, 5000)  # Random multiplier between 50 and 5000
        }
        
        # Connect and send
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            try:
                s.connect((HOST, PORT))
                # Convert dict to JSON string and encode to bytes
                data = json.dumps(stats).encode('utf-8')
                s.sendall(data)
                print(f"Sent: {stats}")
            except Exception as e:
                print(f"Error: {e}")
                break  # Exit if connection fails (e.g., app closed)
        
        # Wait 5 seconds before next message
        time.sleep(5)

if __name__ == "__main__":
    # Initial delay to let RouletteApp start
    print("Waiting 2 seconds before starting...")
    time.sleep(2)
    send_random_message()
    print("Done! (This won't print unless it breaks)")
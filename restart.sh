#!/bin/bash
echo "🔄 Restarting Supermarket API..."

# Định nghĩa ports
HTTP_PORT=5295
HTTPS_PORT=7000

kill_port() {
    local port=$1
    echo "🔍 Checking port $port..."
    
    local pids=$(lsof -ti:$port 2>/dev/null)
    
    if [ ! -z "$pids" ]; then
        echo "⚠️  Found processes on port $port: $pids"
        echo "🔪 Killing processes..."
        
        # Kill từng process
        for pid in $pids; do
            if kill -9 $pid 2>/dev/null; then
                echo "✅ Killed process $pid on port $port"
            else
                echo "❌ Failed to kill process $pid on port $port"
            fi
        done
        
        # Đợi một chút để port được giải phóng
        sleep 2
    else
        echo "✅ Port $port is free"
    fi
}

# Kill ports
kill_port $HTTP_PORT
kill_port $HTTPS_PORT

echo ""
echo "🚀 Starting Supermarket API..."
echo "📱 HTTP:  http://localhost:$HTTP_PORT"
echo "🔒 HTTPS: https://localhost:$HTTPS_PORT"
echo "📚 Swagger: https://localhost:$HTTPS_PORT/swagger"
echo ""

# Start ứng dụng
dotnet run

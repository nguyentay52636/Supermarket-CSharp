#!/bin/bash

# Supermarket API - Run Script
echo "🚀 Starting Supermarket API..."

# Kill any existing processes on port 5295
echo "🔧 Cleaning up existing processes..."
lsof -ti:5295 | xargs -r kill -9 2>/dev/null || true

# Wait a moment for ports to be released
sleep 2

# Check if port is free
if lsof -i :5295 >/dev/null 2>&1; then
    echo "❌ Port 5295 is still in use. Please check manually."
    exit 1
fi

echo "✅ Port 5295 is free"

# Run the application
echo "🏃 Running dotnet run..."
dotnet run --project Supermarket.csproj

echo "👋 Application stopped."
